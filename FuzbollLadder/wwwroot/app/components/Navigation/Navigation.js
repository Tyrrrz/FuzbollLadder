import React from 'react'
import { Link } from 'react-router-dom'
import { Menu } from 'antd'

export default class Navigation extends React.Component {
    render() {
        return (    
            <Menu mode="horizontal" className="mt-3 mb-5" selectedKeys={[location.pathname]}>
                <Menu.Item key="/">
                    <Link className="navbar-brand nav-item" to='/'>Ladder</Link>
                </Menu.Item>
                <Menu.Item key="/players/add"> 
                    <Link className="navbar-brand nav-item" to='/players/add'>Register Player</Link>
                </Menu.Item> 
                <Menu.Item key="/matches"> 
                    <Link className="navbar-brand nav-item" to='/matches'>History</Link>
                </Menu.Item> 
                <Menu.Item key="/matches/add"> 
                    <Link className="navbar-brand nav-item" to='/matches/add'>Submit match</Link>
                </Menu.Item> 
                <Menu.Item key="/playerstats"> 
                    <Link className="navbar-brand nav-item" to='/playerstats'>Statistics</Link>
                </Menu.Item> 
            </Menu> 
        )
    }
}
