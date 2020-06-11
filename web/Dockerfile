FROM node:12.17.0-alpine as build

WORKDIR /app

COPY . /app/

RUN npm install --silent
RUN npm install react-scripts -g --silent

ENTRYPOINT ["npm", "start"] 
