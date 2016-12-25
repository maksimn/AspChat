import React from "react";
import { Link } from "react-router";

import LoginForm from "../../components/LoginForm";
import styles from "./LoginPage.less"

export default class LoginPage extends React.Component {
    render() {
        return (
            <div>
                <div className={ styles.backLinkWrap }>
                    <Link className={ styles.backLink } to="/">Назад</Link>
                </div>
                <div className={ styles.loginArea }>
                    <LoginForm />
                </div>
            </div>
        );
    }
}
