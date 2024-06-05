import type { Metadata } from "next";
import { SubjectMapping } from "../../../../web-api-client";

type Props = {
  params: { id: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const id = params.id;

  const data: SubjectMapping = await fetch(
    `${
      process.env.NODE_ENV == "development"
        ? process.env.WEBSITE_URL_DEV
        : process.env.WEBSITE_URL_RES
    }/api/subject/${id}`
  ).then((res) => res.json());

  return {
    title: data?.name,
  };
}
export default function Layout({ children }: { children: React.ReactNode }) {
  return children;
}
