import LoginForm from "@/app/Login/LoginForm";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import { Button, Divider, Paper } from "@mui/material";

export default function Page() {
	return (
		<Paper
			elevation={3}
			className={
				"flex flex-col mx-auto max-w-md columns-1 items-center gap-5 p-7"
			}
		>
			<AccountCircleIcon sx={{ fontSize: 75 }} />
			<p className="text-3xl">Sign in</p>
			<LoginForm />
			<Divider className={"w-full !mt-3"}>Not a member ?</Divider>
			<Button
				variant="outlined"
				href={"/Register/"}
				endIcon={<ArrowForwardIcon />}
			>
				Sign up
			</Button>
		</Paper>
	);
}
