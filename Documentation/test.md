
Нам необходимо написать `Flatter` (пока внутреннее временное название). Что это такое? Что же, давайте рассмотрим на примере.

_Дальше приведён частный пример, сериализатор должен работать для любых сериализуемых классов._

Пусть у нас будут следующие классы

```csharp
public class Snapshot
{
    public int Number { get; set; }
    public int Md { get; set; }
    public int Pressure { get; set; }
    public Density Density { get; set; }
    public MassFractions MassFractions { get; set; }
}
public class Density
{
    public int Liquid { get; set; }
    public int Gas { get; set; }
}
public class MassFractions
{
    public Component[] Liquid { get; set; }
    public List<Component> Gas { get; set; }
}
public class Component
{
    public string Key { get; set; }
    public double Value { get; set; }
}
```

Создаем объект
```csharp
var snapshot = new Snapshot
{
    Number = 7,
    Md = 120,
    Pressure = 39500000,
    Density = new Density
    {
        Liquid = 518,
        Gas = 50
    },
    MassFractions = new MassFractions()
    {
        Liquid = new Component[]
        {
            new Component
            {
                Key = "6276c48e-83d9-47ec-8d69-5aa623abe950",
                Value = 0.3
            }
        },
        Gas = new List<Component>()
        {
            new Component
            {
                Key = "16abc7fb-5bde-4619-9c36-2329fbc63923",
                Value = 0.5
            }
        }
    }
};
```

Если его сериализовать в json то будет следующая структура
```js
{
    "Number": 7,
    "Md": 120,
    "Pressure": 39500000,
    "Density": {
        "Liquid": 518,
        "Gas": 50
    },
    "MassFractions": {
        "Liquid": [
            {
                "Key": "6276c48e-83d9-47ec-8d69-5aa623abe950",
                "Value": 0.3
            }
        ],
        "Gas": [
            {
                "Key": "16abc7fb-5bde-4619-9c36-2329fbc63923",
                "Value": 0.5
            }
        ]
    }
}
```

Вызываем для этого объекта наш `Flatter` для сериализации и получае два массива: первый - массив строк (заголовков), путей к нашим значениям. второй - массив значений, причем именно значений, НЕ преобразованных в строки

Если их серализовать в json, то будет выглядеть следующим образом

Заголовки
```js
[
    "Number",
    "Md",
    "Pressure",
    "Density.Liquid",
    "Density.Gas",
    "MassFractions.Liquid.[0].Key",
    "MassFractions.Liquid.[0].Value",
    "MassFractions.Gas.[0].Key",
    "MassFractions.Gas.[0].Value"
]
```

Значения
```js
[
    7,
    120,
    39500000,
    518,
    50,
    "6276c48e-83d9-47ec-8d69-5aa623abe950",
    0.3,
    "16abc7fb-5bde-4619-9c36-2329fbc63923",
    0.5
]
```