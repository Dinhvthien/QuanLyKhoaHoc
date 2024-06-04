"use client";

import { Center, Pagination, SimpleGrid } from "@mantine/core";
import RootLayout from "../components/Layout/RootLayout";
import { CourseCard } from "../components/Card/CourseCard";

export default function HomePage() {
  return (
    <RootLayout>
      <SimpleGrid cols={{ base: 1, lg: 3 }} spacing={"xs"}>
        {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((item) => {
          return <CourseCard key={item} />;
        })}
      </SimpleGrid>
      <Center my={"md"}>
        <Pagination total={10} />
      </Center>
    </RootLayout>
  );
}
