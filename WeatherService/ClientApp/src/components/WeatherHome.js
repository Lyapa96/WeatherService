import React, {Component} from 'react';
import {WeatherInfo} from './WeatherInfo';
import ForecastsTable from './ForecastsTable';

export class WeatherHome extends Component {
    static displayName = WeatherHome.name;

    constructor(props) {
        super(props);
        let defaultCity = 'London';
        this.state = {
            forecasts: [],
            loading: true,
            city: defaultCity,
            title: defaultCity,
            status: 'success',
            isInitState: true
        };
    }

    setResponseToState(data, city) {
        this.setState({
            forecasts: data.weatherForecast,
            status: data.status,
            loading: false,
            city: city,
            isInitState: false
        });
    }

    static renderForecasts(forecasts, status, city) {
        //todo: show content depending on the type of status
        return (
            <div>
                <WeatherInfo info={forecasts[0]} city={city}/>
                <ForecastsTable forecasts={forecasts}/>
            </div>
        );
    }

    handleChange(event) {
        this.setState({title: event.target.value})
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : WeatherHome.renderForecasts(this.state.forecasts, this.state.status, this.state.city);
        if (this.state.isInitState)
            contents = (<p>Please, press on button</p>);

        return (
            <div>
                <h1>Weather forecast</h1>
                <input type='text' name='city' value={this.state.title} onChange={this.handleChange.bind(this)}/>
                <button onClick={() => {
                    fetch(`api/Weather/WeatherForecasts?city=${this.state.title}`)
                        .then(response => response.json())
                        .then(data => this.setResponseToState(data, this.state.title));
                }}>Get forecasts
                </button>
                {contents}
            </div>
        );
    }
}
