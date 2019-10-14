import React from 'react';
import { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import BookingInformation from './BookingInformation';
import ParkingExit from './ParkingExit';
import { Button } from 'react-bootstrap';
import Image from 'react-image-resizer';
import { GetData } from './services/GetData';

class App extends Component {
  state = {
    parkingLeft: "Get Current Parking Status"
  };
  constructor(props) {
    super(props)
    this.onClick = this.GetCurrentStatus.bind(this);
  }
  async GetCurrentStatus(event) {
    this.setState(
      {
        parkingLeft: "LoadingData..."
      }
    )

    let data = await GetData('');
    let separator= '-';
    let result = '';
    for(let x of data.data)
    {
      result += x.ParkingType + separator  + x.Count + '     |     ';
    }
       
    this.setState(
      {
        parkingLeft: result
      }
    )
  }

  render() {
    return (
      <Router>
        <div>
          <Image className="logo"
            src={require('./logo.JPG')}
            height={150}
            width={200}
          />
          <h2 align='center'>Welcome to Perched-Peacock-Parking!!!</h2>
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <ul className="navbar-nav mr-auto">
              <li><Link to={'/'} className="nav-link"> Entry </Link></li>
              <li><Link to={'/ParkingExit'} className="nav-link">Exit</Link></li>
              {/* <li><Link to={'/about'} className="nav-link">Get Current Status</Link></li> */}
            </ul>
            <Button type="submit" variant="outline-primary"
              id="blog_post_submit"
              className="btn-default btn"
              align="right"
              onClick={(e) => this.GetCurrentStatus(e)}
            >
              {this.state.parkingLeft}
            </Button>
          </nav>
          <hr />
          <Switch>
            <Route exact path='/' component={BookingInformation} />
            <Route path='/ParkingExit' component={ParkingExit} />
          </Switch>
        </div>
      </Router>
    );
  }
}
export default App;
