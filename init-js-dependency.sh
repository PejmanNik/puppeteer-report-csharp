git submodule update --init
cd src/js-dependency/
echo "PUPPETEER_SKIP_DOWNLOAD=true" >> .npmrc
npm i
npm run build
rm .npmrc