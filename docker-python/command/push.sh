
cd ..

URL=https://github.com/katapy-note/docker-python.git

git init
git checkout -b main
git branch -a
git add .
read -p "Input comment > " COMMENT
git commit -m "$COMMENT"
git push -f --set-upstream $URL main

echo "open $URL"