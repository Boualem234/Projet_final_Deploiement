FROM node:18-alpine

WORKDIR /app

COPY package.json .
RUN npm install

# Copie spécifique du fichier EXE (évite de rebuild si changé)
COPY files/ ./files/

# Copie du reste
COPY . .

EXPOSE 3000
CMD ["node", "app.js"]
