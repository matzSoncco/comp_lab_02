# Sistema Interactivo de Defensa Perimetral 2D
## Laboratorio N° 2 - Computación Gráfica

Este proyecto consiste en el desarrollo de un entorno interactivo bidimensional diseñado bajo el motor de ejecución Unity. El sistema implementa principios fundamentales de computación gráfica, tales como la transformación de matrices en tiempo real, gestión de sistemas de coordenadas cartesianas y optimización de recursos computacionales para hardware de arquitectura integrada

## 1. Descripción del Sistema

El software presenta un escenario de supervivencia donde el usuario controla una unidad central de defensa (Torreta o Nave). El objetivo principal radica en la neutralización de entidades hostiles que convergen hacia el origen de coordenadas, utilizando un sistema de proyectiles balísticos y la recolección estratégica de bonificaciones tácticas

## 2. Mecánicas de Juego y Controles

La interacción del usuario se gestiona íntegramente a través de la interfaz de hardware del ratón, permitiendo una experiencia de control fluida y receptiva

* **Puntería y Orientación:** El sistema de control, programado en `TorretaCotroller.cs`, traduce la posición del cursor en pantalla a coordenadas del mundo virtual. La torreta ejecuta una rotación constante para alinear su eje de disparo con el puntero
* **Ejecución de Disparo:** Mediante el evento de entrada `Input.GetMouseButtonDown(0)` (clic izquierdo), el sistema instancia proyectiles que heredan la trayectoria vectorial de la torreta
* **Supervivencia:** El usuario debe evitar que las entidades gestionadas por `Enemy.cs` entren en contacto con la unidad central

## 3. Dinámica de Entidades Hostiles

El flujo de adversarios es administrado de manera autónoma por el componente `EnemySpawner.cs`. Este módulo ejecuta las siguientes funciones:

* **Generación Perimetral:** Las instancias de enemigos se materializan en coordenadas aleatorias situadas fuera del rango de visión de la cámara para asegurar una dificultad progresiva
* **Vectores de Convergencia:** Cada unidad enemiga calcula un vector de traslación hacia el centro del área de juego, forzando un encuentro cinético con el defensor

## 4. Sistema de Bonificaciones (Power-ups)

Para mitigar el incremento en la densidad de entidades hostiles, se ha implementado un sistema de ventajas tácticas gestionado por `PowerUpSpawner.cs` y ejecutado individualmente por `PowerUp.cs`. Estas bonificaciones aparecen aleatoriamente dentro del campo visual y proporcionan los siguientes beneficios al ser impactadas por un proyectil:

* **Optimización de Cadencia:** Incremento temporal en la velocidad de instanciación de proyectiles considerandolos laser que traspasan enemigos
* **Escalamiento de los Proyectiles:** Modificación de los parámetros de traslación en el script `Projectile.cs` para un escalamiento de los proyectiles haciendo que pueda ocupar más espacio para la colisión con el enemigo
* **Disparo en ángulos:** Proyectil que sale disparado en ángulos de `-15, 0 y 15` para abarcar más espacio de colisión
* **Healing up:** PowerUp que al colisionar con un proyectil añade una vida al jugador, hasta un máximo de 3

## 5. Especificaciones de Optimización

Dada la naturaleza académica del proyecto y las restricciones del hardware de desarrollo (Intel Core i5-8va Gen / Intel UHD 620), el sistema ha sido optimizado mediante:

1.  **Cálculo de Colisiones Simplificado:** Uso exclusivo de `CircleCollider2D` y `PolygonCollider2D` para reducir el gasto de ciclos de CPU
2.  **Gestión de Memoria:** Implementación de rutinas de destrucción inmediata de objetos (`Destroy`) para prevenir fugas de memoria por acumulación de instancias fuera de los límites de la pantalla
3.  **Renderizado Eficiente:** Uso de materiales *Unlit* que no requieren procesamiento de iluminación dinámica, manteniendo una tasa de cuadros por segundo estable

---
**Autor:** Max Junior Soncco Mamani