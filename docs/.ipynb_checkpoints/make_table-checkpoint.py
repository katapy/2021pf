"""
refs
https://datumstudio.jp/blog/1722/
"""

import os
import pandas as pd

input_book = pd.ExcelFile('data_table.xlsx')
sheet_names = input_book.sheet_names

# 各シートの名前を取得
for i, sheet_name in enumerate(sheet_names):
    if sheet_name == 'サンプル' or 'pulldownitem':
        continue
    print ('{0}: {1}'.format(i, sheet_name))

# シートの番号を入力
sheet_num = int(input())

# テーブル名(全て小文字、複数形)
table_name = sheet_names[sheet_num].lower() + "s"

# テーブル情報に変換
table_info = input_book.parse(sheet_name=sheet_names[sheet_num],
skiprows=5,
parse_cols="B:L")

# バックアップファイルを作成する
def backup(file):
    if(os.path.exists(file)):
        os.rename(file, file + ".bak")

# schemasファイルを新規作成する
def make_schemas_file():
    class_name = sheet_names[sheet_num]
    file = '../docker-python/application/api/schemas'
    file += '/{0}.py'.format(class_name.lower())
    backup(file=file)
    f = open(file, 'x')

    f.write('from pydantic import BaseModel\n')
    f.write('\n')
    f.write('class {0}(BaseModel):\n'.format(class_name))

    for name, type in zip(table_info['物理名'], table_info['型名(Python)']):
        if not name:
            continue
        line = "\t"
        line += name
        line += ": "
        line += type
        line += "\n"
        f.write(line)
    f.close()

# modelsクラスを新規作成する
def make_models_file():
    file = '../docker-python/application/api/models'
    file += '/{0}.py'.format(table_name)
    backup(file=file)
    f = open(file, 'x')

    f.write('import sqlalchemy\n')
    f.write('from ..db import metadata, engine\n')
    f.write('\n')
    f.write('{0} = sqlalchemy.Table(\n'.format(table_name))
    f.write('\t\"{0}\",\n'.format(table_name))
    f.write('\tmetadata,\n')

    for name, type in zip(table_info['物理名'], table_info['型名(SQL)']):
        if not name:
            continue
        line = "\tsqlalchemy.Column("
        line += '\"{0}\"'.format(name)
        line += ', sqlalchemy.{0}'.format(type)
        if(table_info['null'][table_info['物理名'] == name].item() == '✖️'):
            line += ', nullable=False'
        if(table_info['primary_key'][table_info['物理名'] == name].item() == '○'):
            line += ', primary_key=True'
        """ 外部キー制約、現在保留中
        if(table_info['Rkey'][table_info['物理名'] == name].item() == '○'):
            line += ', primary_key=True'
        """
        if(table_info['unique'][table_info['物理名'] == name].item() == '○'):
            line += ', unique=True'
        if(table_info['autoincrement'][table_info['物理名'] == name].item() == '○'):
            line += ', autoincrement=True'
        if(table_info['index'][table_info['物理名'] == name].item() == '○'):
            line += ', index=True'

        line += "),\n"

        f.write(line)

    f.write(')\n')
    f.close()

def make_cs_model():
    class_name = sheet_names[sheet_num]
    file = '../UnityConnectedDocker/Assets/Scripts/ConnectServer/Models'
    file += '/{0}.cs'.format(class_name)
    backup(file=file)
    f = open(file, 'x')

    f.write('using System;\n')
    f.write('using System.Collections;\n')
    f.write('using System.Collections.Generic;\n')
    f.write('using UnityEngine;\n')
    f.write('\n')
    f.write('namespace ConnectServer\n')
    f.write('{\n')
    f.write('\t[Serializable]\n')
    f.write('\tpublic class {0} : IModelJsonConvert\n'.format(class_name) )
    f.write('\t{\n')
    for name, type in zip(table_info['物理名'], table_info['型名(C#)']):
        if not name:
            continue
        f.write('\t\tpublic {0} {1};\n'.format(type, name))
    f.write('\n')
    f.write('\t\tpublic string ToJson()\n')
    f.write('\t\t{\n')
    f.write('\t\t\treturn JsonUtility.ToJson(this);\n')
    f.write('\t\t}\n')
    f.write('\n')
    f.write('\t\tpublic void JsonToModel(string json)\n')
    f.write('\t\t{\n')
    f.write('\t\t\tvar m = JsonUtility.FromJson<{0}>(json);\n'.format(class_name))
    f.write('\n')
    for name in table_info['物理名']:
        if not name:
            continue
        f.write('\t\t\tthis.{0} = m.{0};\n'.format(name, name))
    f.write('\t\t}\n')
    f.write('\t}\n')
    f.write('}\n')


# 作成ファイルの番号を入力
print('Python: \t1')
print('SQL: \t2')
print('C#: \t3')
print("Input file number")
file_num = int(input())

if file_num == 1:
    make_schemas_file()
elif file_num == 2:
    make_models_file()
elif file_num == 3:
    make_cs_model()
