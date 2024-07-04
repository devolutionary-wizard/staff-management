import PageContainer from "../../components/container/PageContainer"
import Breadcrumb from "../../components/shared/Breadcrumb"
import StaffScreen from "../../screen/Staff/StaffScreen"

const Staff = () => {
    return <PageContainer title="Staff" description="this is Staff List page">
        <Breadcrumb title="Staff" items={[]} />
        <StaffScreen />
    </PageContainer>
}

export default Staff