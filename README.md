﻿## Чат на ASP.NET MVC

Реализовать:

1. __Выход пользователей из чата.__

   Статус - сделано. Время сеанса пользователя - время одного сеанса браузера. [16 ноября 2016].
   
2. __Перейти к использованию client-side фреймворка React-Redux, webpack.__

   [__Сделано__, 4 декабря 2016]
   
3. Перейти к более приятному дизайну интерфейса пользовательской страницы чата.

   [__Сделано__, 10 декабря 2016]

4. Возможность авторизации пользователя через пару логин/пароль в чате.

   [__Сделано__, 18 декабря 2016]

5. Персистентность данных чата в InMemoryChatRepository между перезапусками веб-сервера.

   [__Сделано__, 19 декабря 2016]

6. Рефакторинг стилей: применение css-препроцессора (stylus / less /sass).

   [__Сделано__, 25 декабря 2016, выбран LESS]

7. Сделать ограничение для длины вводимого пользователем сообщения чата (напр., 500 символов), длины имени пользователя и пароля. 

   [__Сделано__, 25 декабря 2016]

8. Сделать параллельный проект AspWebFormsChat, где реализовать взаимодействие между клиентом и сервером не через WebSocket, а через AJAX и polling / long polling. В качестве фреймворка на клиенте взять jQuery. Здесь уже не будет SPA. Рендер на сервере. Стили -- через Bootstrap. 

9. Реализовать многопоточную синхронизацию доступа к хранилищу сообщений чата.

10. Написать слой работы с данными на Entity Framework и хранить их в SQL Server.

11. Возможность редактирования и удаления пользователем своих сообщений чата.

12. Применить IoC-контейнер (Unity от Microsoft).
