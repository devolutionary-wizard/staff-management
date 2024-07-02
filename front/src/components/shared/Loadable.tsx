import { ComponentType, Suspense } from 'react';
import Spinner from './Spinner';



// eslint-disable-next-line @typescript-eslint/no-explicit-any
const Loadable = <T extends ComponentType<any>>(Component: T) => (props: React.ComponentProps<T>) =>
(
    <Suspense fallback={<Spinner />}>
        <Component {...props} />
    </Suspense>
);

export default Loadable;