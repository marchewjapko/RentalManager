"use client";

import { getSession, useSession } from "next-auth/react";
import { useEffect } from "react";

export default function Home() {
	const lol = useSession({
		required: true,
	});

	useEffect(() => {
		console.log("lol", lol);
	}, [lol]);

	return <div>ASD</div>;
}
