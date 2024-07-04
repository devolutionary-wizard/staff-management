FROM nginx:alpine

WORKDIR /app

COPY deployment/nginx.conf /etc/nginx/conf.d/default.conf 


COPY ./build ./

EXPOSE 80

#Start NGINX when the container starts
CMD ["nginx", "-g", "daemon off;"]