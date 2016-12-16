import React from "react";
import { Link } from "react-router";

import styles from "./RegisterPage.css";

export default class RegisterPage extends React.Component {
    render() {
        return (
            <div>
                <div className={ styles.backLinkWrap }>
                    <Link className={ styles.backLink } to="/">Назад</Link>
                </div>
                <div className={ styles.registerArea }>
                    <form
                          className={ styles.registerForm } method="post" action="register">
                        <div className={ styles.registerFormHeader }>Регистрация в чате</div>
                        <div className={ styles.formField }>
                            <label>Имя:</label><br />
                            <input type="text" required />
                        </div>
                        <div className={ styles.formField }>
                            <label>Пароль:</label><br />
                            <input id="password" type="password" required />                     
                        </div>
                        <div className={ styles.formField }>
                            <label>Повторите пароль:</label><br />
                            <input id="password2" type="password" required />
                        </div>
                        <div className={ styles.formSubmit }>
                            <input type="submit" value="Отправить" />
                        </div>
                    </form>
                </div>
            </div>
        );
    }
}