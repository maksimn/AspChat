import React from "react";

import styles from "./LoginForm.css";

export default class LoginForm extends React.Component {
    render() {
        return (
            <form className={styles.loginForm} method="post" action="login">
                <div className={styles.loginFormHeader}>Вход в чат</div>
                <div className={styles.formField}>
                    <label>Имя:</label><br />
                    <input type="text" name="chatUserName" required />
                </div>
                <div className={styles.formField}>
                    <label>Пароль:</label><br />
                    <input id="password" name="password" type="password" required />
                </div>
                <div className={styles.formSubmit}>
                    <input type="submit" value="Войти" />
                </div>
                <div className={styles.formInputErrors}></div>
            </form>
        );
    }
}