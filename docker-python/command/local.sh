
cd ..

lsof -i:8080

read -p "input PID > " PID
kill -9 $PID

pip install --no-cache-dir -r requirements.txt
uvicorn application.api.main:app --port 8080
