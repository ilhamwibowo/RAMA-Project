# API WEBSERVICE RAMA

## HOW TO START
1. Jalankan Docker Compose
```
    docker compose -f .\docker-compose.yml up 
```
2. Buka localhost:5226/swagger untuk melihat swagger UI dari API


### NOTE
Setelah Melakukan Perubahan pada kode. Rebuild Kontainer dockernya dengan menggunakan.
```
    docker compose -f .\docker-compose.yml build 
```
Lalu jalankan lagi