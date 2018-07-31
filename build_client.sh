rm -r ./wwwroot
mkdir ./wwwroot
tsc --outFile ./wwwroot/main.js ./client_code/main.ts
cp ./client_code/main.css ./wwwroot/main.css
cp ./client_code/index.html ./wwwroot/index.html