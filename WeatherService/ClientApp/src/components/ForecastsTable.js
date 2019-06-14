import React from "react";

export default ({forecasts}) => (
    <table className='table table-striped'>
        <thead>
        <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Wind speed</th>
        </tr>
        </thead>
        <tbody>
        {forecasts.map(forecast =>
            <tr key={forecast.date}>
                <td>{forecast.date}</td>
                <td>{forecast.temp}</td>
                <td>{forecast.windSpeed}</td>
            </tr>
        )}
        </tbody>
    </table>
)