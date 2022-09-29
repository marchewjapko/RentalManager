import {Link} from "react-router-dom";

export default function TempNavigation() {
    return (
        <div>
            <Link to="/">Clients</Link>
            <Link to="/2">RentalEquipment</Link>
            <Link to="/3">Employees</Link>
            <Link to="/4">RentalAgreements</Link>
        </div>
    );
}