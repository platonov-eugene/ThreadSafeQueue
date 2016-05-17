# ThreadSafeQueue <img src="https://raw.githubusercontent.com/platonov-eugene/ThreadSafeQueue/master/ThreadSafeQueue/ThreadSafeQueue.UI/Images/Logotype.png" width="128" height="128" align="right" />
Потокобезопасная очередь элементов ThreadSafeQueue (FIFO) со следующими операциями:
- `void Push(T)` - добавляет элемент типа T в конец коллекции ThreadSafeQueue;
- `T Pop()` - удаляет и возвращает элемент типа T, находящийся в начале коллекции ThreadSafeQueue. В случае, отсутствия элементов типа T в коллекции ThreadSafeQueue, осуществляет ожидание добавления элемента типа T в коллекцию ThreadSafeQueue.

##### Системные требования:
1. Microsoft Visual Studio 2015;
2. Microsoft .NET Framework 4.6.1.

##### Структура файла решения:
1. Проект ThreadSafeQueue.Logic;
2. Проект ThreadSafeQueue.UI.

##### Скриншоты приложения:
<img src="https://raw.githubusercontent.com/platonov-eugene/ThreadSafeQueue/master/Screenshots/Screenshot%20%E2%84%961.png" width="443" height="346" />

<img src="https://raw.githubusercontent.com/platonov-eugene/ThreadSafeQueue/master/Screenshots/Screenshot%20%E2%84%962.png" width="443" height="346" />

<img src="https://raw.githubusercontent.com/platonov-eugene/ThreadSafeQueue/master/Screenshots/Screenshot%20%E2%84%963.png" width="443" height="346" />

##### Загрузка приложения:
<a href="https://github.com/platonov-eugene/ThreadSafeQueue/raw/master/ThreadSafeQueue/ThreadSafeQueue.UI/bin/Debug/ThreadSafeQueue.Logic.dll">ThreadSafeQueue.Logic.dll</a>

<a href="https://github.com/platonov-eugene/ThreadSafeQueue/raw/master/ThreadSafeQueue/ThreadSafeQueue.UI/bin/Debug/ThreadSafeQueue.UI.exe">ThreadSafeQueue.UI.exe</a>
