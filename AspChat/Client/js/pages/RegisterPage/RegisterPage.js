import React from "react";
import { Link } from "react-router";

import styles from "./RegisterPage.css";
import RegistrationForm from "../../components/RegistrationForm";

export default class RegisterPage extends React.Component {
    render() {
        return (
            <div>
                <div className={ styles.backLinkWrap }>
                    <Link to="/">Назад</Link>
                </div>
                <div className={ styles.registerArea }>
                    <RegistrationForm />
                </div>
            </div>
        );
    }
}