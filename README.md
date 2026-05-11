# **1. Description**

`MonoAlphaModifier` is a component for controlling the transparency of visual objects in Unity.

- It manages the transparency of any supported visual components through automatically collected handlers (strategies).
- It can control a single object or an entire hierarchy branch.
- It supports hierarchical behavior: a parent’s alpha value affects all child modifiers.

---

# **2.2 Runtime Behavior**

`MonoAlphaModifier` automatically maintains a correct hierarchy:

- When created, enabled, or moved, the component searches for a parent `MonoAlphaModifier`.
- When removed or re‑parented, all links are rebuilt correctly.
- Any hierarchy change immediately updates the transparency of all affected objects.

Transparency logic:

- Each collected visual component receives its own handler that knows how to apply alpha (e.g., SpriteRenderer, ParticleSystem, Image, etc.).
- The modifier’s own `Alpha` value is multiplied by the parent’s alpha, forming `TotalAlpha`, which is applied to all collected handlers.

---

# **3. Usage**

## **3.1 Editor**

1. Add the `MonoAlphaModifier` component to a GameObject.
2. In the component’s context menu, choose:
   - **Collect for Object** — collect handlers only for this object.
   - **Collect for Branch** — collect handlers for this object and all its children.
3. In the `Alpha Strategies` list, adjust transparency limits for collected components if needed.

## **3.2 Code**

To control transparency from scripts, simply assign a value to the `Alpha` property.

- **Alpha** — the modifier’s own transparency value.
- **TotalAlpha** — the final transparency value after applying all parent modifiers.

To add support for a new type of visual component:

1. Create a class implementing `IAlphaModifierStrategy`.
2. Add the corresponding `builder` to `StrategyBuildContext` so it is automatically included during component scanning.


## Installation

UPM - `https://github.com/CatCodeGames/AlphaModifiers.git?path=Assets/AlphaModifiers`

---



# **1. Описание**

MonoAlphaModifier — компонент для управления прозрачностью визуальных объектов в Unity.
- Управляет прозрачностью любых визуальных компонентов через собранные обработчики (стратегии).
- Может контролировать конкретный объект или всю ветку (в иерархии).
- Поддерживает иерархию: альфа родителя влияет на дочерние модификаторы.

---

# **2. Поведение в рантайме**

`MonoAlphaModifier` автоматически поддерживает иерархию:
- При создании, активации или перемещении компонент ищет родительский `MonoAlphaModifier`.
- При удалении связи так же корректно перестраиваются.
- При изменении иерархии прозрачность объектов сразу же обновляется, учитывая новую иерархию.

Логика изменения прозрачности:
- Для каждого собранного компонента создаётся свой обработчик-стратегия, знающий как менять его прозрачность, будто то Sprite, ParticleSystem или Image.
- Собственное значение `Alpha` умножается на значение родителя формируя `TotalAlpha`, которая применяется как множитель для всех собранных обработчиков.

---

# **3. Использование**

## **3.1 Редактор**
1. Добавить компонент `MonoAlphaModifier` на объект.
2. Выбрать в контекстом меню компонента:
2.1 `Collect for Object` - для контроля прозрачности текущего объекта
2.2 `Collect for Branch` - для контроля прозрачности текущего объекта и дочерних.
3. В списке `Alpha Strategies` при необходимости указать ограничение прозрачности для всех собранных компонентов.

## **3.2 Код**

Для управления прозрачностью через скрипты достаточно задать значение свойства `Alpha`.
- `Alpha` — собственное значение модификатора.
- `TotalAlpha` — итоговое значение с учётом всех родительских модификаторов.
Чтобы добавить поддержку нового типа визуального компонента:
1. Создать класс, реализующий `IAlphaModifierStrategy`.
2. Добавить соответствующий `builder` в `StrategyBuildContext`, чтобы он автоматически собирался при сканировании объектов.


## Установка
UPM - `https://github.com/CatCodeGames/AlphaModifiers.git?path=Assets/AlphaModifiers`

---
