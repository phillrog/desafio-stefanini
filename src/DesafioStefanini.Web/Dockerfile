FROM node:22-alpine AS build
WORKDIR /app
COPY src/DesafioStefanini.Web/package*.json ./

RUN npm install

COPY src/DesafioStefanini.Web/ .

RUN npm run build

FROM nginx:stable-alpine

COPY --from=build /app/dist/DesafioStefanini.Web/browser /usr/share/nginx/html
COPY src/DesafioStefanini.Web/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["/bin/sh", "-c", "sed -i 's|API_URL|'${API_URL}'|g' /usr/share/nginx/html/*.js && nginx -g 'daemon off;'"]