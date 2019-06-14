import React, {Component} from 'react';
import './weather.css';

export class WeatherInfo extends Component {
    constructor(props) {
        super(props);
    }

    getImageByType(type) {
        const pathToImage = '/images/WeatherType/';
        //todo: switch by type after add images
        return pathToImage + 'sun.svg';
    }

    render() {
        return (
            <div className="weather">
                <div className="weather__content">
                    <h1 className="weather__city">{this.props.city}</h1>
                    <img className="weather__img" src={this.getImageByType(this.props.weatherType)} alt="weather type"/>
                </div>
                <div className="weather__info">
                    <div className="weather__today">
                        <div className="weather__temp">
                            <img className="weather__todayTemp" src="/images/temp.svg" alt="weather"/>
                            <span>{this.props.info.temp >= 0 ? '+' : '-'}{this.props.info.temp}&#176;C</span>
                        </div>
                        <div className="weather__windSpeed">
                            <img className="weather__todayWind" src="/images/wind.svg" alt="wind"/>
                            <span>{'\t'}{this.props.info.windSpeed} m/s</span>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
