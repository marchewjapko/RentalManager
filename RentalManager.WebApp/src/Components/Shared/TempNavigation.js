import {Link} from "react-router-dom";

export default function TempNavigation() {
    return (
        <div>
            <Link to="/2">RentalEquipment </Link>
            <Link to="/3">Employees </Link>
            <Link to="/">RentalAgreements</Link>
        </div>
    );
}