docker build -t luckdrawweb .
docker run -d -p 8080:80 --name luckdraw luckdrawweb 