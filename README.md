# 1 Description
`MonoAlphaModifier` lets you control the transparency of various objects, including sprites and particle systems. Its logic is similar to Unity's `CanvasGroup`: it allows you to adjust the alpha value for a group of objects simultaneously, simplifying the management of visual effects.

- Controls the transparency of multiple objects within the `MonoAlphaModifier`.
- Supports nested objects with `MonoAlphaModifier` and combines their alpha values for child objects.

# 2 How to Use
## 2.1 UnityEditor
Add the MonoAlphaModifier component to an object.

Choose one of the two options in the component's context menu:

`Alpha for this Object` - controls the transparency only for the current object.

`Alpha for this Branch` - controls the transparency for the current object and all its child objects.

## 2.2 Scripts
To control transparency, use the `Alpha` property of the `MonoAlphaModifier` component.

To add a new `MonoAlphaModifier` object to the existing hierarchy at runtime, use the methods `FindParent()` or `SetParent(MonoAlphaModifier parent)`.

To remove an object from the hierarchy, use the `RemoveParent()` method.


# 1. Описание
MonoAlphaModifier позволяет контролировать прозрачность различных объектов, включая спрайты и системы частиц. Его логика схожа с `CanvasGroup` в Unity: он позволяет изменять альфа-значение для группы объектов одновременно, что упрощает управление визуальными эффектами.

- Позволяет управлять прозрачностью сразу нескольких объектов, которые находятся внутри `MonoAlphaModifier`.
- Поддерживает вложенные объекты с `MonoAlphaModifier` и комбинирует их альфа-значения для дочерних объектов.

# 2. Как использовать.
## 2.1 UnityEditor

1. Добавить компонент `MonoAlphaModifier` на объект
2. Выбрать одну из двух опций в контекстном меню компонента
2.1 `Alpha for this Object` - компонент будет контролировать прозрачность только для текущего объекта, к которому прикреплён
2.2 `Alpha for this Branch` - компонент будет контролировать прозрачность для текущего и всех дочерних объектов.

## 2.2 Scripts

Для управления прозрачностью используйте свойство `Alpha` компонента `MonoAlphaModifier`.

Если во время выполнения необходимо добавить новый объект `MonoAlphaModifier` в существующую иерархию, используйте методы `FindParent()` или `SetParent(MonoAlphaModifier parent)`.

Для удаления объекта из иерархии используйте метод `RemoveParent()`.
