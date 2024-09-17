import React from 'react';
// import Chart from 'chart.js/auto';
import { Chart, LineController, LineElement, PointElement, LinearScale, Title, ArcElement, CategoryScale} from 'chart.js'
import { Doughnut } from 'react-chartjs-2';

// import { registerables } from 'chart.js';
// export class ChartTest {

//   constructor() {
//     Chart.register(...registerables);
//   }

//   // methods to actually make the chart per documentation
// }
// Chart.register(CategoryScale);
// Chart.register(LinearScale);
// Chart.register(PointElement);
Chart.register(LineController, LineElement, PointElement, LinearScale, Title, ArcElement, CategoryScale);

/**
 * #000000
 * #120305
 * #24060a
 * #36090e
 * #480c13
 * #5a0f18
 * #6b121d
 * #7d1522
 * #8f1826
 * #a11b2b
 * #bb3445
 * #c24b59
 * #ca626e
 * #d17883
 * #d98f98
 * #e1a5ac
 * #e8bcc1
 * #f0d2d6
 * #f7e9ea
 * #ffffff
 */

const state = {
	labels: ['January', 'February', 'March', 'April', 'May'],
	datasets: [
		{
			label: 'Rainfall',
			backgroundColor: [
				'#b31e30',
				'#c24b59',
				'#d98f98',
				'#e8bcc1',
				'#f7e9ea'
			],
			data: [65, 59, 80, 81, 56]
		}
	]
}

export default class App extends React.Component {
	render() {
		return (
			<div>
				<Doughnut
					width={"30%"}
					data={state}
					options={{
						title: {
							display: true,
							text: 'Average Rainfall per month',
							fontSize: 20
						},
						legend: {
							display: true,
							position: 'right'
						},
						maintainAspectRatio: false
					}}
				/>
			</div>
		);
	}
}