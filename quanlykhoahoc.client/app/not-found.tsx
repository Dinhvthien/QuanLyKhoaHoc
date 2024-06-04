import { Error } from "../components/Error/Error";
import RootLayout from "../components/Layout/RootLayout";

export default function NotFound() {
  return (
    <RootLayout>
      <Error status={404}/>
    </RootLayout>
  );
}
