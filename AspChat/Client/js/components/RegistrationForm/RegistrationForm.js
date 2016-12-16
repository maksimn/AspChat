import React from "react";

import styles from "./RegistrationForm.css";

export default class RegistrationForm extends React.Component {
    isFormDataValid() {
        const psw = document.getElementById("password").value;
        const psw1 = document.getElementById("password1").value;
        if (psw && psw1 && (psw !== psw1)) {
            return false;
        }
        return true;
    }

    clientValidation(e) {
        if (!this.isFormDataValid()) {
            e.preventDefault();
            this.showValidationErrors();
        }
    }

    showValidationErrors() {
        let errorsArea = document.querySelector(`.${styles.formInputErrors }`);
        errorsArea.innerHTML = "Введенные значения пароля должны совпадать";
    }

    render() {
        return(
            <form className={styles.registerForm} method="post" action="register">
                <div className={styles.registerFormHeader}>Регистрация в чате</div>
                <div className={styles.formField}>
                    <label>Имя:</label><br />
                    <input type="text" name="chatUserName" required />
                </div>
                <div className={styles.formField}>
                    <label>Пароль:</label><br />
                    <input id="password" name="password" type="password" required />
                </div>
                <div className={styles.formField}>
                    <label>Повторите пароль:</label><br />
                    <input id="password1" type="password" required />
                </div>
                <div className={styles.formSubmit}>
                    <input type="submit" value="Отправить"
                        onClick={this.clientValidation.bind(this)}
                    />
                </div>
                <div className={styles.formInputErrors}></div>
            </form>
        );
    }
}