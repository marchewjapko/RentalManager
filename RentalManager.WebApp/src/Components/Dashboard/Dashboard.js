import { useTranslation } from 'react-i18next';

export default function Dashboard() {
	const { t } = useTranslation(['translation']);

	console.log('T', t);

	return <div>{t('dashboard-title')}</div>;
}
