# TEKUS-APP
Desarrollo Prueba Técnica 

@luguerrero10 

# Prueba técnica C# .NET 💻


## CONOCIMIENTOS FUNDAMENTALES

-Manejo del IDE Visual Studio, de sus herramientas, del entorno de diseño y del entorno de depuración. (Visual Studio 2019 o superior)
-Manejo de C# y de .Net
-Estructuración del código. Buenas prácticas de codificación y documentación
-Programación Orientada a Objetos, programación orientada a eventos
-Manejo de principios S.O.L.I.D
-Manejo de herramientas ORM cómo Entity Framework, Hibernate o similares
-Manejo de sistemas de versionamiento de código y trabajo cooperativo CONOCIMIENTOS DESEABLES
-Diseño de pruebas unitarias sobre .Net
-Manejo básico de tecnologías de frontend: jQuery, Angular, REACT, Vue
-Manejo de componentes de computación en la Nube (Azure, AWS, GCP)
-Manejo de SQL-TRANSACT, SQL SERVER 2018+

## Requerimientos 💡 

-La empresa TEKUS S.A.S. requiere un aplicativo web (.NET Asp.net o .Net Aps.net Core) que permita administrar
(crear, editar y listar) los servicios ofrecidos por sus proveedores. Cada proveedor deberá relacionar los servicios
ofrecidos y a su vez, cada servicio deberá relacionar uno o varios países dónde es ofrecido (por ejemplo, El
proveedor “Importaciones Tekus S.A.” ofrece los servicios “Descarga espacial de contenidos” y “Desaparición
forzada de bytes”, y el servicio “Descarga espacial de contenidos” es ofrecido en Colombia, Perú y México por
el proveedor “Importaciones Tekus S.A.S.”).
- El proveedor deberá tener cómo mínimo los campos ID, NIT, Nombre y correo electrónico.
- El servicio deberá tener cómo mínimo los campos ID, Nombre y valor por hora (en $USD).
- El campo ID en ambas entidades deberá ser un valor autogenerado y único.
- La lista de países deberá ser consultada desde un servicio externo.
- Agregue las columnas que considere necesarias para garantizar la funcionalidad solicitada en los siguientes
puntos.
1. Aunque es un proyecto de una estructura simple, plantea una solución estructurada, separa los
proyectos, considera utilizar patrones de diseño DDD (domain driven design).
2. Deberás proporcionar un mecanismo de autenticación. No es necesario desarrollar ninguna
funcionalidad para crear o administrar usuarios
3. El usuario podrá agregar campos personalizados sobre cada proveedor, por ejemplo, para el proveedor
“Importaciones Tekus S.A” se podría agregar el campo “Número de contacto en marte” o el campo
“Cantidad de mascotas en la nómina”.
4. Deberás proveer un endpoint del API que entregue un resumen que permita visualizar 2 indicadores que
consideres importante mostrar, por ejemplo: a) Cantidad de clientes por país, b) Cantidad de servicios
por país
