import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import { Button, Divider, Paper } from "@mui/material";
import RegisterForm from "@/app/register/RegisterForm";

export default function Page() {
	return (
		<Paper
			elevation={3}
			className={
				"flex flex-col mx-auto max-w-md columns-1 items-center gap-5 p-7"
			}
		>
			<AccountCircleIcon sx={{ fontSize: 75 }} />
			<p className="text-3xl">Sign up</p>
			<RegisterForm />
			<Divider className={"w-full !mt-3"}>Already a member ?</Divider>
			<Button
				variant="outlined"
				href={"/login/"}
				endIcon={<ArrowForwardIcon />}
			>
				Sign in
			</Button>
		</Paper>
	);
}
