# Taller de lenguajes 1 - RPG - Cassas Tomas
## Metadata
Este proyecto fué presentado el día viernes 28 de Julio en el laboratorio B, turno tarde.

## Concepto
Este RPG trata sobre un torneo de 10 luchadores generados de forma aleatoria, tanto nombres como estadisticas.

El torneo inicia leyendo el contenido de un archivo autogenerado `personajes.json` (en el cual se encuentran la descripción de todos los luchadores), ordenandolos de forma aleatoria e iniciando una pelea 1v1: el primero contra el segundo, el tercero contra el cuarto... etc.

La peleas son automaticas, por turnos, en las cuales el ganador de cada combate recibirá un bonus de salud de 10 puntos, y tendrá la posibilidad de elegir una **bebida de recompenza**, la cual aumentará/disminuirá sus estadisticas basado en su `resistenciaAlAlcohol`. Si los luchadores eligen beber agua (25% de posiblidades) tendrán un poderoso bonus de estadisticas.

## Uso de API
Para obtener información de la bebida brindada a los luchadores al ganar un batalla se realiza una request a una API de bebidas, la cual retorna una bebida aleatoria basado en una lista de ingredientes (elegida aleatoriamente por el propio torneo).

La API es: https://api-ninjas.com/api/cocktail

