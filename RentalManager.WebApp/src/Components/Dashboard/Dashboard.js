import { useTranslation } from 'react-i18next';

export default function Dashboard() {
	const { t } = useTranslation(['translation']);

	return <div>{t('dashboard-title')}</div>;
}
