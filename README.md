# **The Odds Api**

![The Odds Api](https://the-odds-api.com/assets/img/logo.png)

***Esta app es un ejemplo de uso del Api de The Odds Api, para obtener datos de las cuotas de diferentes casas de apuestas deportivas.***

* Para su realización usamos el API de Odds API.
> Sports betting API covering odds from bookmakers around the world.

Disponible en: 
[THE ODDs API ](https://the-odds-api.com/)

## **RESULTADOS OBTENIDOS**
He adaptado el formulario para faciliar el uso del API y presentar en pantalla eventos deportivos específicos. Los resultados obtenidos al final, se guardan internamente en una lista que luego puede ser usada para realizar arbitraje o algún modelo matemático para realizar apuestas.

![Screenshot of app](Arbitraje/bin/Debug/net7.0-windows/img/main.png)
 
### **Uso del API**
```c#
 // Ejemplo API:
 // https://api.the-odds-api.com/v4/sports/soccer_conmebol_copa_libertadores/odds/?apiKey=3f415e1e00c15eea72cf0cf501ac225a&regions=eu&markets=h2h
 var url = $"{baseUrl}/sports/{sport}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
 var response = await _client.GetAsync(url);
 response.EnsureSuccessStatusCode();
 jsonText = await response.Content.ReadAsStringAsync();

 // Al final los datos se almacenan en un txt 
 File.WriteAllText($"{sport}.txt", jsonText);
```

### **El repositorio original se hizo con fines de realizar arbitraje, pero se deja a conveniencia el uso que le pueden dar al mismo.**
