"use client";

import { Flex, Loader } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout";

export default function Loading() {
  return (
    <DashboardLayout>
      <Flex h={"100vh"} justify={"center"} align={"center"}>
        <Loader />
      </Flex>
    </DashboardLayout>
  );
}
