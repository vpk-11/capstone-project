import React from 'react';
import './Feature.css';
import DoughChart from './Charts/doughnutChart';
import LineChart from './Charts/lineChart';

const GridExample = () => {
	return (
		<div className="container">
			<div className="row">
				<h2>Feature Title</h2>
				<div className="col-3">
					<span className='stat-heading'>Pool Value</span>
					<span className='stat-value'>$80M</span>
				</div>
				<div className="col-3">
					<span className='stat-heading'>No of Holdings</span>
					<span className='stat-value'>$80M</span>
				</div>
				<div className="col-3">
					<span className='stat-heading'>Stat 3</span>
					<span className='stat-value'>$80M</span>
				</div>
				<div className="col-3">
					<span className='stat-heading'>Stat 4</span>
					<span className='stat-value'>$80M</span>
				</div>
			</div>
			<div className="row">
				<div className="col-4"><span className='stat-heading'>Description</span>
					<p style={{ textAlign: 'justify' }}>
						Lorem ipsum dolor sit amet, consectetur adipiscing elit.
						Ut scelerisque est sit amet nunc efficitur vestibulum.
						Ut vel dignissim eros. Sed condimentum, erat nec posuere blandit, nisi nulla lacinia neque,
						quis interdum leo nisi nec ligula.
						Ut pretium erat non vestibulum luctus. In imperdiet vitae ex a viverra. Donec venenatis pulvinar orci ut auctor.
					</p>
				</div>
				<div className="col-4">
					<span className='stat-heading'>Seller Compostion</span>
					<DoughChart />
				</div>
				<div className="col-4">
					<span className='stat-heading'>State Compostion</span>
					<DoughChart />
				</div>
			</div>
			<div className="row">
				<div className="col-6">
					<span className='stat-heading'>SMM / CPR</span>
					<div className='f'><LineChart /></div>
				</div>
				<div className="col-6">
					<span className='stat-heading'>Features</span>
					<table className='table'>
						<tr>
							<td>1</td>
							<td>1</td>
						</tr>
						<tr>
							<td>2</td>
							<td>2</td>
						</tr>
						<tr>
							<td>3</td>
							<td>3</td>
						</tr>
						<tr>
							<td>4</td>
							<td>4</td>
						</tr>
						<tr>
							<td>5</td>
							<td>5</td>
						</tr>
						<tr>
							<td>6</td>
							<td>6</td>
						</tr>
					</table>
				</div>
			</div>
		</div>
	);
};

export default GridExample;