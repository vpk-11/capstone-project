import React from 'react';
import {Line} from 'react-chartjs-2';
import { Chart, LineController, LineElement, PointElement, LinearScale, Title , ArcElement, CategoryScale} from 'chart.js'

Chart.register(LineController, LineElement, PointElement, LinearScale, Title, ArcElement, CategoryScale);

const state = {
  labels: ['January', 'February', 'March',
           'April', 'May'],
  datasets: [
    {
      label: 'Rainfall',
      fill: false,
      lineTension: 0.5,
      backgroundColor: 'white',
      borderColor: '#b31e30',
      borderWidth: 2,
      data: [65, 59, 80, 81, 56]
    }
  ]
}

export default class App extends React.Component {
  render() {
    return (
      <div>
        <Line
          data={state}
          options={{
            title:{
              display:true,
              text:'Average Rainfall per month',
              fontSize:20
            },
            legend:{
              display:true,
              position:'right'
            }
          }}
        />
      </div>
    );
  }
}