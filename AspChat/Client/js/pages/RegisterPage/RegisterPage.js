import React from "react";
import { Link } from "react-router";

import styles from "./RegisterPage.css";

export default class RegisterPage extends React.Component {
    render() {
        return (
            <div>
                <header className={ styles.header }>
                    <div className={ styles.chatLabel }>Чат</div>
                    <div className={ styles.headerLinks }>
                        <Link to="/">Назад</Link>
                    </div> 
                </header>
            </div>
        );
    }
}