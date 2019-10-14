import * as React from 'react';


const Navbar = () => {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div className="navbar Header">
                    <a className="nav-item nav-link active" href="./BookingInformation" Link To = "./BookingInformation">Entry </a>
                    {/* <span className="sr-only">(current)</span></a> */}
                    <a className="nav-item nav-link" href="./ParkingExit">Exit</a>                   
                </div>
            </div>
        </nav>
    )
}

export default Navbar;

