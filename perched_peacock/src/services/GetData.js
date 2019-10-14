import axios from 'axios';

export async function GetData(data) {
      
    const config = {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
      let res =  {};
      await axios.get("http://localhost:5074/api/ParkingSystem/" + data, config)             
      .then(function (response) {
          res=response;
         
          console.warn(response);
      })
      .catch(function (response) {
         
          console.warn(response);
      });
    return(res);    
}