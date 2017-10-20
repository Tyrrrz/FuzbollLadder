import React from 'react';
import { Link } from 'react-router-dom'

export default () => {
    return (
        <nav className="navbar navbar-toggleable-md navigation navbar-light">
            <Link className="navbar-brand nav-item" to='/'>Ladder</Link>
            <Link className="navbar-brand nav-item" to='/players/add'>Register Player</Link>
            <Link className="navbar-brand nav-item" to='/matches'>History</Link>
            <Link className="navbar-brand nav-item" to='/matches/add'>Submit match</Link>
        </nav>
    );
};