FROM python:3.9.7

WORKDIR /app

RUN /usr/local/bin/python -m pip install --upgrade pip
COPY requirements.txt /usr/src/requirements.txt
RUN pip install --no-cache-dir -r /usr/src/requirements.txt

COPY ./application/ ./application/
EXPOSE 8000
# FastAPIを8000ポートで待機
CMD ["uvicorn", "application.api.main:app", "--reload", "--host", "0.0.0.0", "--port", "8000"]