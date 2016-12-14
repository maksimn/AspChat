import React from 'react';
import { Link } from 'react-router';

import styles from "./StartPage.css"

export default class StartPage extends React.Component {
    render() {
        return (
            <div className={ styles.body }>
                <header className={ styles.header }>
                    <div className={ styles.chatLabel }>Чат</div>
                    <div className={ styles.headerLinks }>
                        <Link className={ styles.enterLink } to="login">
                            Войти
                        </Link>
                        <Link className={ styles.registerLink } to="register">
                            Регистрация
                        </Link>
                    </div> 
                </header>
            </div>
        );
    }
}
