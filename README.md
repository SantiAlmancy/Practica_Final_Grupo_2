**PRÁCTICA FINAL GRUPO 2**

**Participantes:**

Becerra Almancy Santiago Mena Angulo Santiago Andres Vargas Rojas Nicolas Villarroel Claros Adriana

**PARTES MÁS IMPORTANTES DEL PROYECTO**

**AMBIENTES:**

Cada ambiente tiene su propio título y este irá cambiando dependiendo el ambiente en el que estemos. También se encuentra la ubicación de nuestro archivo products.json

**Development**

En este ambiente se utiliza el paquete Serilog para la configuración del registro de logs. Se configura para que los logs se escriban tanto en la consola como en un archivo .log.

**QA**

En este ambiente se utiliza el paquete Serilog para la configuración del registro de logs. Se configura para que los logs se escriban únicamente en un archivo .log.

**UAT**

En este ambiente se utiliza el paquete Serilog para la configuración del registro de logs. Se configura para que los logs se escriban únicamente en consola.

**MÉTODOS IMPORTANTES: *Create***

Verificamos que el tipo de producto ingresado sea SOCCER o BASKET, luego para leer todo el archivo .json utilizamosFile.ReadAllText luego para convertir todos los datos de tipo json a Tipo List de products utilizamosJsonSerializer.Deserialize luego trabajamos con las lista obtenida. Para poner el código de producto con su número incremental correspondiente buscamos el número mayor del mismo tipo ya sea SOCCER o BASKET para luego sumarle uno. Luego creamos el producto, lo añadimos a la lista y esa lista la convertimos a un tipo json conJsonSerializer.Serialize y lo volvemos a escribir conFile.WriteAllText.

***Read***

**GetAll (Leer todos los productos)**

Este método devuelve como resultado una lista de objetos Product a partir del archivo products.json. En primer lugar, se verifica si el path donde se encuentran los productos existe, en caso de no existir, se devuelve una lista vacía de productos “return new List<Product>();”. En caso de existir el archivo, se procede a leer su contenido. La línea “string json = File.ReadAllText(\_path);” lee todo el contenido del archivo y lo guarda en una variable llamada json. Luego, se utiliza “JsonSerializer.Deserialize<List<Product>>(json)” para deserializar el contenido JSON en una lista de objetos Product. Finalmente, esta lista deserializada se devuelve como resultado del método.

**GetByCode(string code)**

El método GetByCode recibe un código como argumento y devuelve un objeto Product. Primero, verifica si la longitud del código proporcionado es diferente de 10 caracteres. Si la longitud del código no es 10, se genera una excepción indicando que el código no es válido. Si la longitud del código es válida, se lee el contenido de un archivo. A continuación, se convierte el contenido leído, que está en formato JSON, en una lista de objetos Product. Se itera sobre cada objeto Product en la lista y se verifica si el código del producto coincide con el código proporcionado. Si se encuentra un producto con el código coincidente, se devuelve ese objeto Product. Si no se encuentra ningún producto con el código proporcionado, se genera una excepción indicando que no se encontró ningún producto con ese código.

***Update***

En este código, se lee el contenido de un archivo JSON y se deserializa en una lista de objetos Product. Luego, se verifica si existe un producto con un código específico en la lista. Si se encuentra, se actualizan las propiedades del producto y se guarda la lista actualizada en el archivo JSON. Si no se encuentra ningún producto con el código especificado, se lanza una excepción.

***Delete***

En la clase ProductManager.cs se agrega el DeleteByCode donde se recibe el código a eliminar como un string, primero verificamos el tamaño de del código puesto que nunca sea diferente de 10 luego leemos el archivo json para luego convertirlo a una lista de productos con JsonSerializer.Deserialize, luego trabajamos con lógica de listas para buscar el objeto que queremos eliminar y lo eliminamos de la lista. Finalmente convertimos la lista de productos a tipo json con JsonSerializer.Serialize y lo reescribimos obteniendo así la nueva lista actualizada con el producto indicado eliminado.

**Price Generator - backing service**

**PutAllPrices**

El código lee un archivo JSON y lo convierte en una lista de objetos Product. Luego, recorre cada objeto Product en la lista y verifica si el precio es igual a cero. Si lo es, llama a un servicio de manera asíncrona para obtener un nuevo precio aleatorio para ese producto. Una vez que se recibe el nuevo precio, se actualiza el objeto Product correspondiente.

Después de actualizar todos los precios, la lista de objetos Product actualizada se convierte de nuevo a formato JSON y se guarda en el archivo original, sobrescribiendo el contenido anterior. Finalmente, el método devuelve la lista de productos actualizada. En resumen, el código actualiza los precios de los productos en un archivo JSON utilizando un servicio asíncrono y guarda los cambios en el archivo.

**PutPrice**

Primero se leen los datos JSON de un archivo y los guarda en una variable como una cadena. Luego, se convierte la cadena JSON en una lista de objetos "Product" y se busca un producto en la lista que coincida con el código especificado. Si se encuentra el producto, se actualiza su precio llamando a un servicio para obtener un nuevo precio. Después de actualizar el precio, la lista de productos se convierte de nuevo a formato JSON y se guarda en el archivo. Finalmente, se devuelve el producto actualizado.

**getRandom**

Este método crea un cliente HTTP llamado sharedClient utilizando la clase HttpClient. Luego, configura la dirección base del cliente y realiza una solicitud GET a través del método GetAsync(""). El código captura la respuesta de la solicitud utilizando un bloque using y verifica si la solicitud fue exitosa. Luego, obtiene el contenido de la respuesta en formato JSON y lo deserializa en un objeto JsonElement. A partir de este objeto, se extrae el valor de la propiedad "decimal" y se convierte en un double, que finalmente se devuelve.
