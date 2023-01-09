FROM node:19-alpine AS builder
WORKDIR /app
COPY package.json package.json
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
RUN echo 'Europe/Prague' > /etc/timezone
WORKDIR /etc/nginx/conf.d
COPY default.conf .
WORKDIR /usr/share/nginx/html
RUN rm -rf *
COPY --from=builder /app/dist .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]