git submodule update --init --remote
cd src/js-dependency/
echo "PUPPETEER_SKIP_DOWNLOAD=true" >> .npmrc
npm i
npm run build-bundle
rm .npmrc