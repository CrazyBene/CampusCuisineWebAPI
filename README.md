# Campus Cuisine

Campus Cuisine ist ein Beispielprojekt, das zeigt, wie man mit ASP.Net Core eine Web-API mit C# erstellt.

## Step 0: Empty Project

In diesem ersten Schritt wird die Web-API selbst erstellt und gestartet. Es gibt vorerst keine Möglichkeit, mit ihr zu interagieren, und sie tut von sich auch nichts selber, außer Ressourcen zu verbrauchen.

## Step 1: Minimal API

Für diesen anfänglichen Test mit der Minimal API gibt es 4 Endpunkte, um mit der WebAPI zu interagieren. Sie folgen keine API-Architektur und sind nur aus Beispielzwecken so implementiert worden.

## Step 2: Restful API

Es sind nochmal weitere Endpunkte dazugekommen und alle Endpunkte wurde nach der REST-API-Architektur Restful implementiert.

## Step 3: Controller

Die Rezept Endpunkte wurden in ihrem eigenen Controller zusammengeführt. Dies macht das ganze nicht nur übersichtlicher, sondern erlaubt später auch weitere Optimierungen.

## Step 4: Persistent data

Bisher wurden die Daten nur in Variablen gespeichert, was dazu führte, dass sie bei jedem Neustart der Anwendung verloren gingen. Um dieses Problem zu lösen, wird jetzt eine Datenbank integriert. Diese ermöglicht es, die Daten auf der Festplatte zu speichern und dauerhaft verfügbar zu halten. So bleibt die Datenintegrität auch nach einem Neustart erhalten und sorgt für eine langfristige, zuverlässige Speicherung.
