import { Link } from 'react-router-dom';

export default function TempNavigation() {
	// return <div></div>;

	return (
		<div>
			<Link to="/rental-equipment">RentalEquipment </Link>
			{' | '}
			<Link to="/employees">Employees </Link>
			{' | '}
			<Link to="/">RentalAgreements</Link>
		</div>
	);
}
