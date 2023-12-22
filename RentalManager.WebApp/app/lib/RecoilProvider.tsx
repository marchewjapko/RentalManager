import React from "react";

export default function RecoilProvider({
	children,
}: {
	children: React.ReactNode;
}) {
	return <RecoilProvider>{children}</RecoilProvider>;
}
