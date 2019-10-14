// import fetch from 'isomorphic-fetch';
import axios from 'axios';
import qs from 'querystring';
// * snip *

export async function PostData(data) {
      
    const config = {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
      let res =  {};
      await axios.post("http://localhost:5074/api/ParkingSystem", qs.stringify(data), config)             
        .then(function (response) {
            res=response;            
            console.warn(response);
        })
        .catch(function (response) {
            
            console.warn(response);
        })
    return(res); 
}