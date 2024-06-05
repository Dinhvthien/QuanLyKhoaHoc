import type { Metadata } from "next";
import { CourseMapping } from "../../../web-api-client";

type Props = {
  params: { id: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const id = params.id;

  const data: CourseMapping = await fetch(
    `${process.env.WEBSTE_URL}/api/course/${id}`
  ).then((res) => res.json());

  return {
    title: data?.name,
  };
}
export default function Layout({ children }: { children: React.ReactNode }) {
  return children;
}
